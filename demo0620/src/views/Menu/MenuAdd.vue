<template>
    <div>
        <el-form ref="form" :model="form" label-width="80px">
            <el-form-item label="菜单名称">
           <el-input v-model="form.MenuName"></el-input>
        </el-form-item>
        <el-form-item label="父级菜单：" >
             <el-cascader
            :options="options"
            :props="{ checkStrictly: true }"
            clearable
            ref="getParentId"></el-cascader>
        </el-form-item>
        <el-form-item label="路径Url">
           <el-input v-model="form.LinkUrl"></el-input>
        </el-form-item>
        
        <el-form-item>
            <el-button type="primary" @click="onSubmit">立即创建</el-button>
            <el-button>取消</el-button>
        </el-form-item>
        </el-form>
    </div>
</template>

<script>
  export default {
    data() {
      return {
        form: {
          MenuName:'',
          LinkUrl:'',
          ParentId:''
        },
        options:[]
      }
    },
    methods: {
      onSubmit() {
        this.$refs.form.resetFields();
        this.form.ParentId=this.$refs["getParentId"].getCheckedNodes()[0].value;
        debugger;
           this.$axios.post("/api/Menu/GetMenuAdds",this.form).then(
                    res=>{
                      console.log(res);
                         if(res.data)
                     {
                        this.$message.success("添加成功");
                        this.$emit("Add");
                     }
                     else
                     {
                        this.$message.error("添加失败");
                     }
              })
      },
      getMenu(){
                debugger;
                this.$axios.get("/api/Menu/GetMenuAdd").then(
                    res=>{
                    var reg = new RegExp('\\,"children":\\[]', 'g')
                    this.options = JSON.parse(JSON.stringify(res.data).replace(reg, ''))
              })
      },
    },
    created(){
        this.getMenu();
    }
  }
</script>