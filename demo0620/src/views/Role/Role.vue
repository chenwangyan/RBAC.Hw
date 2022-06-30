  <template>
    <div>
      <div> 
          
      </div>
       <el-table
      :data="RoleData"
      style="width: 100%">
      <el-table-column
        prop="RoleName"
        label="用户名"
        width="180">
      </el-table-column>

       <el-table-column
        label="账号创建时间"
        width="180">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.CreateTime | TimeFil }}</span>
        </template>
      </el-table-column>

    <el-table-column label="操作">
      <template slot-scope="scope">
        <el-button
          size="mini"
          type="primary"
          @click="getAllot(scope.row.RoleId)">分配权限</el-button>
        <el-button
          size="mini"
          @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
        <el-button
          size="mini"
          type="danger"
          @click="handleDelete(scope.$index, scope.row)">删除</el-button>
      </template>
    </el-table-column>
    </el-table>

    <el-dialog
        title="权限分配"
        :visible.sync="dialogVisible"
        width="30%"
        >
        <tree ref="treenode" ></tree>
        <span slot="footer" class="dialog-footer">
            <el-button @click="dialogVisible = false">取 消</el-button>
            <el-button type="primary" @click="getNodeId">确 定</el-button>
        </span>
    </el-dialog>

    <!-- 分页 -->
    <el-pagination
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
      :current-page="form.pageIndex"
      :page-sizes="[1, 2, 3, 4]"
      :page-size="100"
      layout="total, sizes, prev, pager, next, jumper"
      :total="totalCount">
    </el-pagination>

    </div>
  </template>

  <script>
    import tree from '../Tree.vue'
    
    export default {
      components: {
        tree,
      },
      data() {
        return {
          RoleData: [],
          form:{
            pageIndex:1,
            pageSize:2
          },
          totalCount:0,
          dialogVisible:false,
          MenuRole:{
            RoleId:0,
            MenuId:[]
          }
        }
      },
      methods:{
        getRole(){
            this.$axios.post("/api/Role/GetRolePage",this.form).then(res=>{
                this.RoleData=res.data.Item1;
                this.totalCount=res.data.Item2;
            })
        },
        //分配权限
        getAllot(RoleId){
            this.MenuRole.RoleId=RoleId;
            this.dialogVisible=true;
        },
        getNodeId(){
           this.MenuRole.MenuId= this.$refs["treenode"].$refs["treedate"].getCheckedNodes(true,true).map(m=>m.value);
            this.$axios.post("/api/Role/GetAddMenuRole",this.MenuRole).then(res=>{
                this.$message({
                message: '分配成功',
                type: 'success',
                onClose:(m=>{
                  this.getRole();
                })
              });
            })
           this.dialogVisible=false;
        },
         handleSizeChange(val) {
          debugger;
          this.form.pageIndex=1;
          this.form.pageSize=val;
          this.getRole();
        },
        handleCurrentChange(val) {
          debugger;
          this.form.pageIndex=val;
          this.getRole();
        },
        handleEdit(){

        },
        handleDelete(){
            
        }
      },
      created(){
        this.getRole();
      },
      filters:{
        TimeFil(val){
          return val.substring(0,10)
        }
      }
    }
  </script>