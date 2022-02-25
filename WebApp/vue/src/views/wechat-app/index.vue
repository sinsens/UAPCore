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
      width="500px"
    >
      <el-form
        ref="form"
        :inline="true"
        :model="form"
        :rules="rules"
        size="small"
        label-width="100px"
      >
        <el-form-item label="名称" prop="name">
          <el-input v-model="form.name" style="width: 350px;" />
        </el-form-item>
        <el-form-item
          label="AppId"
          prop="appId"
        >
          <el-input v-model="form.appId" style="width: 350px;" />
        </el-form-item>
        <el-form-item label="AppSecret" prop="appSecret">
          <el-input
            v-model="form.appSecret"
            style="width: 350px;"
            type="textarea"
          />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button
          size="small"
          v-loading="formLoading"
          type="primary"
          @click="save"
        >
          确认
        </el-button>
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
      <el-table-column label="名称" prop="name" sortable="custom" align="center" width="150px">
        <template slot-scope="{row}">
          <span class="link-type" @click="handleUpdate(row)">{{row.name}}</span>
        </template>
      </el-table-column>
      <el-table-column label="AppId" prop="appId" align="center">
            <template slot-scope="scope">
              <span>{{scope.row.appId}}</span>
            </template>
          </el-table-column>
      <el-table-column label="创建日期" prop="creationTime" sortable="custom" align="center" width="200px">
        <template slot-scope="scope">
          <span>{{scope.row.creationTime | formatDate}}</span>
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
import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";

const weChatAppType =
{
    MiniProgram : 0,
    Official : 1,
    Work : 2,
    OpenPlatform : 3
}

const defaultForm = {
  id: null,
  type: weChatAppType.MiniProgram,
  name: '',
  displayName: '',
  appId: '',
  appSecret: '',
  token: '',
  encodingAesKey: ''
};
export default {
  name: "WechatApp",
  components: { Pagination },
  directives: { permission },
  data() {
    return {
      rules: {
        name: [{ required: true, message: "请输入名称", trigger: "blur" }],
        appId: [{ required: true, message: "请输入AppID", trigger: "blur" }],
        appSecret: [{ required: true, message: "请输入AppSecret", trigger: "blur" }],
      },
      form: Object.assign({}, defaultForm),
      list: null,
      totalCount: 0,
      listLoading: true,
      formLoading: false,
      listQuery: {
        Filter: "",
        Sorting: "",
        SkipCount: 0,
        MaxResultCount: 10,
      },
      page: 1,
      dialogFormVisible: false,
      multipleSelection: [],
      formTitle: "",
      isEdit: false,
    };
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios
        .gets("/api/wechat-management/common/wechat-app", this.listQuery)
        .then((response) => {
          this.list = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
    },
    fetchData(id) {
      this.$axios.gets("/api/wechat-management/common/wechat-app/" + id).then((response) => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    save() {
      this.$refs.form.validate((valid) => {
        if (valid) {
          this.formLoading = true;
          this.form.roleNames = this.checkedRole;
          if (this.isEdit) {
            this.$axios
              .puts("/api/wechat-management/common/wechat-app/" + this.form.id, this.form)
              .then((response) => {
                this.formLoading = false;
                this.$notify({
                  title: "成功",
                  message: "更新成功",
                  type: "success",
                  duration: 2000,
                });
                this.dialogFormVisible = false;
                this.getList();
              })
              .catch(() => {
                this.formLoading = false;
              });
          } else {
            this.$axios
              .posts("/api/wechat-management/common/wechat-app", this.form)
              .then((response) => {
                this.formLoading = false;
                this.$notify({
                  title: "成功",
                  message: "新增成功",
                  type: "success",
                  duration: 2000,
                });
                this.dialogFormVisible = false;
                this.getList();
              })
              .catch(() => {
                this.formLoading = false;
              });
          }
        }
      });
    },
    handleCreate() {
      this.formTitle = "新增应用";
      this.isEdit = false;
      this.dialogFormVisible = true;
    },
    handleDelete(row) {
      this.$confirm("是否删除" + row.name + "?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          this.$axios
            .deletes("/api/wechat-management/common/wechat-app/" + row.id)
            .then((response) => {
              this.$notify({
                title: "成功",
                message: "删除成功",
                type: "success",
                duration: 2000,
              });
              this.getList();
            });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除",
          });
        });
    },
    handleUpdate(row) {
      this.formTitle = "修改应用";
      this.isEdit = true;
      if (row) {
        this.fetchData(row.id);
        this.dialogFormVisible = true;
      } else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: "编辑必须选择单行",
            type: "warning",
          });
          return;
        } else {
          this.fetchData(this.multipleSelection[0].id);
          this.dialogFormVisible = true;
        }
      }
    },
    sortChange(data) {
      const { prop, order } = data;
      if (!prop || !order) {
        this.handleFilter();
        return;
      }
      this.listQuery.Sorting = prop + " " + order;
      this.handleFilter();
    },
    cancel() {
      this.form = Object.assign({}, defaultForm);
      this.dialogFormVisible = false;
      this.$refs.form.clearValidate();
    },
  },
};
</script>
