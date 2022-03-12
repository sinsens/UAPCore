<template>
  <div class="app-container">
    <div class="head-container">
      <el-input v-model="listQuery.Filter" clearable size="small" placeholder="搜索手机号码或姓名拼音" style="width: 200px;"
        class="filter-item" @keyup.enter.native="handleFilter" @change="handleFilter" />
      <el-select v-model="listQuery.Status" @change="handleFilter">
        <el-option v-for="item in statusOptions" :key="item.id" :value="item.id" :label="item.name" />
      </el-select>
      <el-button class="filter-item" size="mini" type="success" icon="el-icon-search" @click="handleFilter">
        搜索
      </el-button>
      <div style="padding: 6px 0;">
        <el-button class="filter-item" size="mini" type="primary" icon="el-icon-plus" @click="handleCreate">
          新增
        </el-button>
        <el-button class="filter-item" size="mini" type="primary" icon="el-icon-plus" @click="handleQuickCreate">
          快速新增
        </el-button>
      </div>
    </div>

    <!-- 新增窗口 -->
    <el-dialog :visible.sync="dialogFormVisible" :close-on-click-modal="false" :title="formTitle" @close="cancel()">
      <el-form ref="form" :inline="true" :model="form" :rules="rules" size="small" label-width="100px">
        <el-form-item label="所属应用" prop="appId">
          <el-select v-model="form.appId" style="width: 188px;" placeholder="请选择" @change="changeApp" :disabled="isEdit"
            filterable default-first-option>
            <el-option v-for="item in appList" :key="item.id" :label="item.name" :value="item.id"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="姓名" prop="name">
          <el-input v-model="form.name" style="width: 350px;" />
        </el-form-item>
        <el-form-item label="联系电话" prop="phone">
          <el-input v-model="form.phone" style="width: 350px;" />
        </el-form-item>
        <el-form-item label="储物柜" prop="lockerIds">
          <el-select v-model="form.lockerIds" multiple style="width: 188px;" placeholder="请选择" filterable
            default-first-option clearable>
            <el-option v-for="item in lockerList" :key="item.id" :value="item.id" :label="'编号' + item.name">
              编号{{ item.number }}
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="起租时间" prop="rentTime">
          <el-date-picker v-model="form.rentTime" type="datetime" />
        </el-form-item>
        <el-form-item label="备注" prop="remark">
          <el-input v-model="form.remark" style="width: 350px;" type="textarea" />
        </el-form-item>
        <el-form-item v-if="isEdit" label="退租时间" prop="returnTime">
          <el-date-picker v-model="form.returnTime" type="datetime" />
        </el-form-item>
        <el-form-item v-if="isEdit" label="退租备注" prop="returnRemark">
          <el-input v-model="form.returnRemark" style="width: 350px;" type="textarea" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="save">
          确认
        </el-button>
      </div>
    </el-dialog>
    <!-- end of 新增窗口 -->

    <!-- 快速新增窗口 -->
    <el-dialog :visible.sync="dialogQuickApplyFormVisible" :close-on-click-modal="false" title="快速新增租用申请"
      @close="closeQuickApplyForm">
      <el-form ref="form" :inline="true" :model="form" :rules="rules" size="small" label-width="100px">
        <el-form-item label="所属应用" prop="appId">
          <el-select v-model="form.appId" style="width: 188px;" placeholder="请选择" @change="changeApp" :disabled="isEdit"
            filterable default-first-option>
            <el-option v-for="item in appList" :key="item.id" :label="item.name" :value="item.id"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="姓名" prop="name">
          <el-input v-model="form.name" style="width: 350px;" />
        </el-form-item>
        <el-form-item label="联系电话" prop="phone">
          <el-input v-model="form.phone" style="width: 350px;" />
        </el-form-item>
        <el-form-item v-if="lockerList.length > 0" label="分配数量">
          <el-input @change="quickSelectLockers" :min="1" :max="999" v-model="num" label="输入要分配的数量"></el-input>
        </el-form-item>
        <el-form-item v-if="lockerList.length > 0" label="常用数量">
          <el-tag effect="dark" v-for="i in [1, 2, 3, 4, 5, 10, 20]" :key="i" @click="quickSelectLockers(i)"
            :type="num == i ? 'success' : ''">
            {{ i }}
          </el-tag>
        </el-form-item>
        <el-form-item label="储物柜" prop="lockerIds">
          <el-select v-model="form.lockerIds" multiple style="width: 188px;" placeholder="请选择" filterable
            default-first-option clearable>
            <el-option v-for="item in lockerList" :key="item.id" :value="item.id" :label="'编号' + item.name">
              编号{{ item.number }}
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="起租时间" prop="rentTime">
          <el-date-picker v-model="form.rentTime" type="datetime" />
        </el-form-item>
        <el-form-item label="备注" prop="remark">
          <el-input v-model="form.remark" style="width: 350px;" type="textarea" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" type="text" @click="closeQuickApplyForm">
          取消
        </el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="quickSave">
          确认
        </el-button>
      </div>
    </el-dialog>
    <!-- end of 快速新增窗口 -->

    <!-- 查看窗口 -->
    <el-dialog :visible.sync="detailFormVisible" :close-on-click-modal="false" title="出租详情">
      <el-descriptions class="margin-top" :title="form.statusDesc" :column="2" border>
        <template slot="extra">
          <el-button type="danger" @click="handleDiscard" size="small" v-if="form.status != 10" v-loading="formLoading"
            :v-permission="[
              'SharedLocker.LockerRent.Create',
              'SharedLocker.LockerRent.Update',
            ]">
            作废
          </el-button>
          <el-button type="primary" @click="handleReturn" size="small" v-loading="formLoading" v-if="form.status == 1">
            退租
          </el-button>
        </template>
        <el-descriptions-item span="2">
          <template slot="label">
            所属应用
          </template>
          {{ form.appName }}
        </el-descriptions-item>
        <el-descriptions-item>
          <template slot="label">
            <i class="el-icon-user"></i>
            租用人姓名
          </template>
          {{ form.name }}
        </el-descriptions-item>
        <el-descriptions-item>
          <template slot="label">
            <i class="el-icon-microphone"></i>
            姓名拼音
          </template>
          {{ form.fullPinyinName }}
        </el-descriptions-item>
        <el-descriptions-item>
          <template slot="label">
            <i class="el-icon-mobile-phone"></i>
            联系电话
          </template>
          {{ form.phone }}
        </el-descriptions-item>
        <el-descriptions-item>
          <template slot="label">
            <i class="el-icon-time"></i>
            起租时间
          </template>
          {{ form.rentTime | formatDateTime }}
        </el-descriptions-item>
        <el-descriptions-item span="2">
          <template slot="label">
            <i class="el-icon-tickets"></i>
            租用储物柜
          </template>
          <el-tag size="small" v-for="locker in form.lockers" :key="locker.id">
            编号{{ locker.number }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item span="2">
          <template slot="label">
            <i class="el-icon-notebook-1"></i>
            备注
          </template>
          {{ form.remark }}
        </el-descriptions-item>
        <el-descriptions-item span="2">
          <template slot="label">
            <i class="el-icon-time"></i>
            退租时间
          </template>
          {{ form.returnTime | formatDateTime }}
        </el-descriptions-item>
        <el-descriptions-item span="2">
          <template slot="label">
            <i class="el-icon-notebook-1"></i>
            退租备注
          </template>
          {{ form.returnMark }}
        </el-descriptions-item>
      </el-descriptions>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" type="text" @click="closeDetail">
          关闭
        </el-button>
      </div>
    </el-dialog>
    <!-- end of 查看窗口 -->

    <!-- 退租窗口 -->
    <el-dialog :visible.sync="returnFormVisible" :close-on-click-modal="false" title="退租" @close="cancelReturn">
      <el-descriptions border :column="2">
        <el-descriptions-item labelStyle="width:120px">
          <template slot="label">
            <i class="el-icon-user"></i>
            租用人姓名
          </template>
          {{ form.name }}
        </el-descriptions-item>
        <el-descriptions-item>
          <template slot="label">
            <i class="el-icon-microphone"></i>
            姓名拼音
          </template>
          {{ form.fullPinyinName }}
        </el-descriptions-item>
        <el-descriptions-item>
          <template slot="label">
            <i class="el-icon-mobile-phone"></i>
            联系电话
          </template>
          {{ form.phone }}
        </el-descriptions-item>
        <el-descriptions-item>
          <template slot="label">
            <i class="el-icon-time"></i>
            起租时间
          </template>
          {{ form.rentTime | formatDateTime }}
        </el-descriptions-item>
        <el-descriptions-item span="2">
          <template slot="label">
            <i class="el-icon-tickets"></i>
            租用储物柜
          </template>
          <el-tag size="small" v-for="locker in form.lockers" :key="locker.id">
            编号{{ locker.number }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item span="2">
          <template slot="label">
            <i class="el-icon-notebook-1"></i>
            备注
          </template>
          {{ form.remark }}
        </el-descriptions-item>

        <el-descriptions-item span="2" label="退租信息">
          <el-form ref="form" :inline="true" :model="form" :rules="returnRules" size="small" label-width="100px">
            <el-form-item v-if="form.id != ''" label="退租时间" prop="returnTime">
              <el-date-picker v-model="form.returnTime" type="datetime" />
            </el-form-item>
            <el-form-item v-if="form.id != ''" label="备注" prop="returnRemark">
              <el-input v-model="form.returnRemark" style="width: 350px;" type="textarea" />
            </el-form-item>
          </el-form>
        </el-descriptions-item>
      </el-descriptions>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" type="text" @click="cancelReturn">
          取消
        </el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="returnRent" :v-permission="[
            'SharedLocker.LockerRent.Create',
            'SharedLocker.LockerRent.Update',
          ]" v-if="form.status == 1">
          确认
        </el-button>
      </div>
    </el-dialog>
    <!-- end of 退租窗口 -->

    <el-table ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="width: 90%;"
      @sort-change="sortChange">
      <el-table-column label="租用人姓名" prop="name" sortable="custom" align="center" width="180px">
        <template slot-scope="{ row }">
          <span class="link-type" @click="handleUpdate(row)">
            {{
              row.name + (row.fullPinyinName ? ` [${row.fullPinyinName}]` : '')
            }}
          </span>
        </template>
      </el-table-column>

      <el-table-column label="起租时间" prop="rentTime" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.rentTime | formatDateTime }}</span>
        </template>
      </el-table-column>

      <el-table-column label="退租时间" prop="returnTime" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.returnTime | formatDateTime }}</span>
        </template>
      </el-table-column>

      <el-table-column label="状态" prop="statusDesc" sortable="custom" align="center" width="100px">
        <template slot-scope="scope">
          <span>{{ scope.row.statusDesc }}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作" align="center">
        <template slot-scope="{ row }">
          <el-button type="info" size="mini" @click="handleUpdate(row)" icon="el-icon-view" />
          <el-button v-if="row.status == 1" type="primary" size="mini" @click="handleOpenReturnDialog(row)"
            icon="el-icon-check" />
          <el-button v-if="row.status == 10" type="danger" size="mini" @click="handleDelete(row)" icon="el-icon-delete"
            :v-permission="['SharedLocker.LockerRent.Delete']" />
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="totalCount > 0" :total="totalCount" :page.sync="page" :limit.sync="listQuery.MaxResultCount"
      @pagination="getList" />
  </div>
</template>

<script>
  import Pagination from '@/components/Pagination'
  import permission from '@/directive/permission/index.js'

  const statusOptions = [{
      id: null,
      name: '全部',
    },
    {
      id: 1,
      name: '租用中',
    },
    {
      id: 8,
      name: '已结束',
    },
    {
      id: 10,
      name: '已作废',
    },
  ]

  const defaultForm = {
    appId: '',
    name: '',
    phone: '',
    rentTime: '',
    remark: '',
    lockerIds: [],
  }
  export default {
    name: 'LockerRents',
    components: {
      Pagination
    },
    directives: {
      permission
    },
    data() {
      return {
        rules: {
          appId: [{
            required: true,
            message: '请选择所属应用',
            trigger: 'blur'
          }],
          name: [{
            required: true,
            message: '请输入姓名',
            trigger: 'blur'
          }],
          phone: [{
              required: true,
              message: '请输入联系电话',
              trigger: 'blur'
            },
            {
              type: 'string',
              message: '请输入正确的手机号码',
              required: true,
              pattern: /^1[3-9]{1}\d{9}$/,
            },
          ],
          rentTime: [{
            required: true,
            message: '请选择起租时间',
            trigger: 'blur'
          }, ],
          mark: [],
          lockerIds: [{
            required: true,
            message: '请选择储物柜',
            trigger: 'blur'
          }, ],
        },
        returnRules: {
          returnTime: [{
            required: true,
            message: '请选择退租时间',
            trigger: 'blur'
          }, ],
          returnMark: [],
        },
        statusOptions: statusOptions,
        form: Object.assign({}, defaultForm),
        list: null,
        appList: [],
        lockerList: [],
        totalCount: 0,
        listLoading: true,
        formLoading: false,
        listQuery: {
          Status: null,
          Filter: '',
          Sorting: '',
          SkipCount: 0,
          MaxResultCount: 10,
        },
        lockerListQuery: {
          AppId: '',
          Status: 0,
          Filter: '',
          Sorting: '',
          SkipCount: 0,
          MaxResultCount: 50,
        },
        appListQuery: {
          Filter: '',
          Sorting: '',
          SkipCount: 0,
          MaxResultCount: 100,
        },
        num: 1,
        page: 1,
        dialogFormVisible: false,
        multipleSelection: [],
        formTitle: '',
        isEdit: false,
        detailFormVisible: false,
        returnFormVisible: false,
        dialogQuickApplyFormVisible: false,
      }
    },
    created() {
      this.getList()
      this.getAppList()
      this.getLockers()
    },
    computed: {
      assignNum() {
        return this.form.lockerIds.length
      }
    },
    methods: {
      quickSelectLockers(currentValue) {
        this.num = currentValue
        let selectedNum = 0
        let {
          lockerIds
        } = this.form
        if (this.assignNum == this.num) {
          return
        }
        if (this.assignNum < this.num) {
          for (const locker of this.lockerList) {
            if (!lockerIds.includes(locker.id)) {
              lockerIds.push(locker.id)
            }
            if (this.assignNum == this.num) {
              break
            }
          }
        } else if (this.assignNum > this.num) {
          while (this.assignNum > this.num) {
            lockerIds.pop()
          }
        }

        if (this.assignNum != this.num) {
          this.$message.warning(`可用储物柜不足 ${this.num}个 `)
        }
      },
      closeQuickApplyForm() {
        this.dialogQuickApplyFormVisible = false
        this.$refs.form.clearValidate()
      },
      cancelReturn() {
        this.returnFormVisible = false
      },
      closeDetail() {
        this.detailFormVisible = false
      },
      changeApp(val) {
        this.lockerListQuery.AppId = val
        this.getLockers()
      },
      getAppList() {
        this.$axios
          .gets('/api/wechat-management/common/wechat-app', this.listQuery)
          .then((response) => {
            this.appList = response.items
          })
      },
      getLockers() {
        this.$axios
          .gets('/api/shared-locker/locker/getlist', this.lockerListQuery)
          .then((response) => {
            this.lockerList = response.items
          })
      },
      getList() {
        this.listLoading = true
        this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount
        this.$axios
          .gets('/api/shared-locker/rent/getlist', this.listQuery)
          .then((response) => {
            this.list = response.items
            this.totalCount = response.totalCount
            this.listLoading = false
          })
      },
      fetchData(id) {
        this.formLoading = true
        this.$axios.gets('/api/shared-locker/rent/get/' + id).then((response) => {
          this.form = response
          for (const app of this.appList) {
            if (app.id == response.appId) {
              this.form.appName = app.name
              break
            }
          }
          this.formLoading = false
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
                .puts('/api/shared-locker/rent/update/' + this.form.id, this.form)
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
              this.sumbitApply()
            }
          }
        })
      },
      sumbitApply() {
        this.formLoading = true
        this.$axios
          .posts('/api/shared-locker/rent/rent', this.form)
          .then((response) => {
            this.formLoading = false
            this.$notify({
              title: '成功',
              message: '新增成功',
              type: 'success',
              duration: 2000,
            })
            this.dialogFormVisible = false
            this.dialogQuickApplyFormVisible = false
            this.getList()
          })
          .catch(() => {
            this.formLoading = false
          })
      },
      quickSave() {
        this.$refs.form.validate((valid) => {
          if (valid) {
            if (this.num != this.form.lockerIds.length) {
              this.$confirm(
                `储物柜分配数量(${this.num})与可用数量不一致(${this.form.lockerIds.length})，是否继续提交?`,
                '提示', {
                  confirmButtonText: '确定',
                  cancelButtonText: '取消',
                  type: 'warning',
                },
              ).then(() => {
                this.sumbitApply()
              })
            } else {
              this.sumbitApply()
            }
          }
        })
      },
      handleReturn() {
        this.returnFormVisible = true
      },
      handleOpenReturnDialog(row) {
        if (row) {
          this.fetchData(row.id)
          this.returnFormVisible = true
        }
      },
      handleDiscard() {
        this.$confirm('是否作废?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning',
        }).then(() => {
          this.$axios
            .puts('/api/shared-locker/rent/discard/' + this.form.id)
            .then((response) => {
              this.$notify({
                title: '成功',
                message: '作废成功',
                type: 'success',
                duration: 2000,
              })
              this.getList()
            })
        })
      },
      returnRent() {
        this.$refs.form.validate((valid) => {
          if (valid) {
            this.$confirm('确认退租?', '提示', {
              confirmButtonText: '确定',
              cancelButtonText: '取消',
              type: 'warning',
            }).then(() => {
              this.$axios
                .puts('/api/shared-locker/rent/return/' + this.form.id, this.form)
                .then((response) => {
                  this.$notify({
                    title: '成功',
                    message: '退租成功',
                    type: 'success',
                    duration: 2000,
                  })
                  this.getList()
                  this.detailFormVisible = false
                  this.returnFormVisible = false
                })
            })
          }
        })
      },
      resetForm() {
        for (const i in this.form) {
          if (i == 'lockerIds') this.form[i] = []
          if (i == 'rentTime') this.form[i] = ''
          else if (typeof this.form[i] == 'string') this.form[i] = ''
        }
        if (this.$refs.form) this.$refs.form.clearValidate()
        this.lockerList = []
      },
      handleCreate() {
        this.formTitle = '新增租用申请'
        this.isEdit = false
        this.resetForm()
        this.dialogFormVisible = true
      },
      handleQuickCreate() {
        this.resetForm()
        this.dialogQuickApplyFormVisible = true
        this.form['rentTime'] = new Date()
      },
      handleDelete(row) {
        this.$confirm('是否删除这条记录?', '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning',
          })
          .then(() => {
            this.$axios
              .deletes('/api/shared-locker/rent/delete/' + row.id)
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
        if (row) {
          this.fetchData(row.id)
          this.detailFormVisible = true
        } else {
          if (this.multipleSelection.length != 1) {
            this.$message({
              message: '编辑必须选择单行',
              type: 'warning',
            })
            return
          } else {
            this.fetchData(this.multipleSelection[0].id)
            this.detailFormVisible = true
          }
        }
      },
      sortChange(data) {
        const {
          prop,
          order
        } = data
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

<style>
  .el-dialog {
    min-width: 370px;
  }

  .el-input__inner {
    width: 200px
  }
</style>
