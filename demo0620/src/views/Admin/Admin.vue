  <template>
    <div>
      <div> 
          
      </div>
       <el-table
      :data="AdminData"
      style="width: 100%">
      <el-table-column
        prop="UserName"
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
    </el-table>

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
    export default {
      data() {
        return {
          AdminData: [],
          form:{
            pageIndex:1,
            pageSize:2
          },
          totalCount:0
        }
      },
      methods:{
        getAdmin(){
            this.$axios.post("/api/Admin/GetAdminPage",this.form).then(res=>{
                this.AdminData=res.data.Item1;
                this.totalCount=res.data.Item2;
            })
        },
         handleSizeChange(val) {
          debugger;
          this.form.pageIndex=1;
          this.form.pageSize=val;
          this.getAdmin();
        },
        handleCurrentChange(val) {
          debugger;
          this.form.pageIndex=val;
          this.getAdmin();
        }
      },
      created(){
        this.getAdmin();
      },
      filters:{
        TimeFil(val){
          return val.substring(0,10)
        }
      }
    }
  </script>