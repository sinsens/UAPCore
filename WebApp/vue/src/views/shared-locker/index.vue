<template>
  <div class="app-container">
    <div class="head-container">
      <el-input
        v-model="listQuery.Filter"
        clearable
        size="small"
        placeholder="搜索..."
        style="width: 200px;"
        class="filter-item"
        @keyup.enter.native="handleFilter"
      />
      <el-select v-model="listQuery.status" @change="handleFilter">
        <el-option
          v-for="item in lockerStatusOptions"
          :key="item.id"
          :value="item.id"
          :label="item.name"
        />
      </el-select>
      <el-button
        class="filter-item"
        size="mini"
        type="success"
        icon="el-icon-search"
        @click="handleFilter"
      >
        搜索
      </el-button>
      <div style="padding: 6px 0;">
        <el-button
          class="filter-item"
          size="mini"
          type="primary"
          icon="el-icon-plus"
          @click="handleCreate"
          :v-permission="['SharedLocker.Locker.Create']"
        >
          新增
        </el-button>
      </div>
    </div>

    <el-dialog
      :visible.sync="dialogFormVisible"
      :close-on-click-modal="false"
      :title="formTitle"
      @close="cancel()"
      width="800px"
    >
      <el-form
        ref="form"
        :inline="true"
        :model="form"
        :rules="rules"
        size="small"
        label-width="100px"
      >
        <el-form-item label="所属应用" prop="appId">
          <el-select
            v-model="form.appId"
            style="width: 188px;"
            placeholder="请选择"
            :disabled="form.id && form.id.length > 0"
          >
            <el-option
              v-for="item in appList"
              :key="item.name"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="名称" prop="name">
          <el-input v-model="form.name" style="width: 350px;" />
        </el-form-item>
        <el-form-item label="编号" prop="number">
          <el-input v-model="form.number" style="width: 350px;" type="number" />
        </el-form-item>
        <el-form-item label="状态" prop="status">
          <el-select v-model="form.status" style="width: 188px;">
            <el-option
              v-for="item in lockerStatusOptions"
              :key="item.name"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
      </el-form>
      <div style="text-align: right;">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button
          size="small"
          v-loading="formLoading"
          type="primary"
          @click="save"
          :v-permission="[
            'SharedLocker.Locker.Create',
            'SharedLocker.Locker.Update',
          ]"
        >
          确认
        </el-button>
      </div>

      <div slot="footer" class="dialog-footer">
        <el-table
          :data="rentList"
          stripe
          style="width: 100%;"
          row-key="id"
          :default-sort="{ prop: 'rentTime', order: 'descending' }"
        >
          <el-table-column
            prop="rentTime"
            label="起租时间"
            width="180"
            :formatter="formatTime"
          ></el-table-column>
          <el-table-column
            prop="name"
            label="租用人"
            width="180"
          ></el-table-column>
          <el-table-column
            prop="returnTime"
            :formatter="formatTime"
            label="归还时间"
          ></el-table-column>
          <el-table-column prop="statusDesc" label="状态"></el-table-column>
        </el-table>
        <pagination
          v-show="rentlistQuery.totalCount >= rentlistQuery.MaxResultCount"
          :total="rentlistQuery.totalCount"
          :page.sync="rentlistQuery.page"
          :limit.sync="rentlistQuery.MaxResultCount"
          @pagination="getRentList"
        />
      </div>
    </el-dialog>

    <el-table
      ref="multipleTable"
      v-loading="listLoading"
      :data="list"
      size="small"
      style="width: 90%;"
      @sort-change="sortChange"
    >
      <el-table-column label="编号" prop="number" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.number }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="名称"
        prop="name"
        sortable="custom"
        align="center"
        width="150px"
      >
        <template slot-scope="{ row }">
          <span class="link-type" @click="handleUpdate(row)">
            {{ row.name }}
          </span>
        </template>
      </el-table-column>

      <el-table-column
        label="状态"
        prop="statusDesc"
        sortable="custom"
        align="center"
        width="200px"
      >
        <template slot-scope="scope">
          <el-tag
            type="success"
            effect="dark"
            size="small"
            v-if="scope.row.status == 0"
          >
            {{ scope.row.statusDesc }}
          </el-tag>
          <el-tag
            type="warning"
            effect="dark"
            size="small"
            v-if="scope.row.status == 1"
          >
            {{ scope.row.statusDesc }}
          </el-tag>
          <el-tag
            type="danger"
            effect="dark"
            size="small"
            v-if="scope.row.status == 2"
          >
            {{ scope.row.statusDesc }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column label="操作" align="center">
        <template slot-scope="{ row }">
          <el-button
            type="primary"
            size="mini"
            @click="handleUpdate(row)"
            icon="el-icon-edit"
          />
          <el-button
            type="danger"
            size="mini"
            @click="handleDelete(row)"
            icon="el-icon-delete"
            :v-permission="['SharedLocker.Locker.Delete']"
          />
        </template>
      </el-table-column>
    </el-table>

    <pagination
      v-show="totalCount > 0"
      :total="totalCount"
      :page.sync="page"
      :limit.sync="listQuery.MaxResultCount"
      @pagination="getList"
    />
  </div>
</template>

<script>
import Pagination from '@/components/Pagination'
import permission from '@/directive/permission/index.js'
import { formatDateTime } from '@/utils/formator.js'

const lockerStatus = [
  {
    id: null,
    name: '全部',
  },
  {
    id: 0,
    name: '闲置',
  },
  {
    id: 1,
    name: '使用中',
  },
  {
    id: 2,
    name: '维护中',
  },
]

const defaultForm = {
  name: '',
  number: 1,
  isActive: true,
  appId: null,
}
export default {
  name: 'Lockers',
  components: { Pagination },
  directives: { permission },
  data() {
    return {
      rules: {
        appId: [{ required: true, message: '请选择所属应用', trigger: 'blur' }],
        name: [{ required: true, message: '请输入名称', trigger: 'blur' }],
        number: [{ required: true, message: '请输入编号', trigger: 'blur' }],
      },
      lockerStatusOptions: lockerStatus,
      form: Object.assign({}, defaultForm),
      list: null,
      appList: [],
      totalCount: 0,
      listLoading: true,
      formLoading: false,
      listQuery: {
        Status: [],
        Filter: '',
        Sorting: '',
        SkipCount: 0,
        MaxResultCount: 10,
      },
      appListQuery: {
        Filter: '',
        Sorting: '',
        SkipCount: 0,
        MaxResultCount: 100,
      },
      rentlistQuery: {
        SkipCount: 0,
        MaxResultCount: 10,
        page: 1,
        totalCount: 0,
      },
      page: 1,
      dialogFormVisible: false,
      multipleSelection: [],
      formTitle: '',
      isEdit: false,
      rentList: [],
    }
  },
  created() {
    this.getList()
    this.getAppList()
  },
  methods: {
    formatTime(row, column, cellValue, index) {
      return formatDateTime(cellValue)
    },
    getAppList() {
      this.$axios
        .gets('/api/wechat-management/common/wechat-app', this.listQuery)
        .then((response) => {
          this.appList = response.items
        })
    },
    getList() {
      this.listLoading = true
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount
      this.$axios
        .gets('/api/shared-locker/locker/getlist', this.listQuery)
        .then((response) => {
          this.list = response.items
          this.totalCount = response.totalCount
          this.listLoading = false
        })
    },
    getRentList(id, isNew) {
      if (isNew) {
        this.rentlistQuery = {
          SkipCount: 0,
          MaxResultCount: 10,
          page: 1,
          totalCount: 0,
        }
      }
      this.rentlistQuery.SkipCount =
        (this.rentlistQuery.page - 1) * this.rentlistQuery.MaxResultCount
      this.$axios
        .gets(
          `/api/shared-locker/locker/getrentlist/${id || this.form.id}`,
          this.rentlistQuery,
        )
        .then((response) => {
          this.rentList = response.items
          this.rentlistQuery.totalCount = response.totalCount
        })
    },
    fetchData(id) {
      this.getRentList(id, true)
      this.$axios
        .gets('/api/shared-locker/locker/get/' + id)
        .then((response) => {
          this.form = response
          /*
          this.form.rentInfos = response.rentInfos.sort((a, b) => {
            return new Date(b.rentTime) - new Date(a.rentTime)
          })*/
        })
    },
    handleFilter() {
      this.page = 1
      this.getList()
    },
    save() {
      this.$refs.form.validate((valid) => {
        if (valid) {
          this.formLoading = true
          this.form.roleNames = this.checkedRole
          if (this.isEdit) {
            this.$axios
              .puts(
                '/api/shared-locker/locker/update/' + this.form.id,
                this.form,
              )
              .then((response) => {
                this.formLoading = false
                this.$notify({
                  title: '成功',
                  message: '更新成功',
                  type: 'success',
                  duration: 2000,
                })
                this.dialogFormVisible = false
                this.getList()
              })
              .catch(() => {
                this.formLoading = false
              })
          } else {
            this.$axios
              .posts('/api/shared-locker/locker/create', this.form)
              .then((response) => {
                this.formLoading = false
                this.$notify({
                  title: '成功',
                  message: '新增成功',
                  type: 'success',
                  duration: 2000,
                })
                this.dialogFormVisible = false
                this.getList()
              })
              .catch(() => {
                this.formLoading = false
              })
          }
        }
      })
    },
    handleCreate() {
      this.formTitle = '新增储物柜'
      this.isEdit = false
      this.dialogFormVisible = true
    },
    handleDelete(row) {
      this.$confirm('是否删除' + row.name + '?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning',
      })
        .then(() => {
          this.$axios
            .deletes('/api/shared-locker/locker/delete/' + row.id)
            .then((response) => {
              this.$notify({
                title: '成功',
                message: '删除成功',
                type: 'success',
                duration: 2000,
              })
              this.getList()
            })
        })
        .catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除',
          })
        })
    },
    handleUpdate(row) {
      this.formTitle = '修改储物柜'
      this.isEdit = true
      if (row) {
        this.fetchData(row.id)
        this.dialogFormVisible = true
      } else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: '编辑必须选择单行',
            type: 'warning',
          })
          return
        } else {
          this.fetchData(this.multipleSelection[0].id)
          this.dialogFormVisible = true
        }
      }
    },
    sortChange(data) {
      const { prop, order } = data
      if (!prop || !order) {
        this.handleFilter()
        return
      }
      this.listQuery.Sorting = prop + ' ' + order
      this.handleFilter()
    },
    cancel() {
      this.form = Object.assign({}, defaultForm)
      this.dialogFormVisible = false
      this.$refs.form.clearValidate()
    },
  },
}
</script>
