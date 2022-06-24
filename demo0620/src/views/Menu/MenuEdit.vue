<template>
    <div>
        
        <el-form ref="form" :model="form" label-width="80px">
            <el-form-item label="菜单名称">
           <el-input v-model="form.MenuName"></el-input>
        </el-form-item>
        <el-form-item label="父级菜单：" >
             <el-cascader
             v-model="form.ParentId"
            :options="options"
            :props="{ checkStrictly: true }"
            ref="getParentId"
            :change="getChange"
            clearable
            ></el-cascader>
        </el-form-item>
        <el-form-item label="路径Url">
           <el-input v-model="form.LinkUrl"></el-input>
        </el-form-item>
        
        <el-form-item>
            <el-button type="primary" @click="getEdit">立即修改</el-button>
            <el-button>取消</el-button>
        </el-form-item>
        </el-form>
    </div>
</template>

<script>
  export default {
    props:["formdate"],
    data() {
      return {
        form: {
          MenuId:'',
          MenuName:'',
          LinkUrl:'',
          ParentId:''
        },
        options:[]
      }
    },
    mounted(){
       
    },
    watch:{
        formdate(val)
        {
          this.getMenu();
        }
    },
    methods: {
      getChange(){
        
      },
      getMenu(){
              debugger;
            this.form.MenuId=this.formdate.MenuId;
            this.form.MenuName=this.formdate.MenuName;
            this.form.LinkUrl=this.formdate.LinkUrl;
            this.form.ParentId=this.formdate.MenuId;
              this.$axios.get("/api/Menu/GetMenuAdd").then(
                  res=>{
                    console.log(res);
                    this.options = res.data;
            })
      },
      getEdit(){
        debugger;
        this.form.ParentId=this.$refs["getParentId"].checkedValue[this.$refs["getParentId"].checkedValue.length-1];
        if(this.form.ParentId==undefined)
        {
              this.form.ParentId=0;
        }
         this.$axios.post("/api/Menu/GetMenuEdit",this.form).then(
                  res=>{
                   if(res.data)
                     {
                        this.$message.success("修改成功");
                        this.$emit("Edit");
                     }
                     else
                     {
                        this.$message.error("修改失败");
                     }
            })
      }
    },
    created(){
        this.getMenu();
    }
  }
</script>