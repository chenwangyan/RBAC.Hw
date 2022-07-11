<template>
    <div>
    
     <el-form ref="form" :model="form" label-width="80px">
         <el-form-item label="角色">
             <el-select v-model="form.RoleId" multiple placeholder="请选择">
                <el-option
                v-for="item in options"
                :key="item.RoleId"
                :label="item.RoleName"
                :value="item.RoleId">
                </el-option>
            </el-select>
        </el-form-item>
      <el-form-item>
            <el-button type="primary" @click="onSubmit">确认分配权限</el-button>
        </el-form-item>
     </el-form>
    </div>
</template>


<script>
  export default {
    props:["AdminId"],
    data() {
      return {
        options: [],
        form:{
            AdminId:0,
            RoleId: [],
        }
      }
    },
    methods:{
        getRole(){
            this.$axios.get("/api/Role/GetList").then(res=>{
                this.options=res.data;
            })
        },
        onSubmit(){
            this.form.AdminId=this.AdminId;
            this.$axios.post("/api/Role/GetAddAdminRole",this.form).then(res=>{
                if(res.data>0)
                {
                    this.$message.success("用户分配权限成功");
                    this.$emit("getSure");
                }
                else
                {
                    
                }
            })
        }
    },
    created(){
        this.getRole();
    }
  }
</script>