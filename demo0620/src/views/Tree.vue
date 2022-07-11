<template>
  <div>
    <el-tree
    ref="treedate"
        :data="data"
        show-checkbox
        node-key="id"
        :default-expand-all="true"
        :default-checked-keys="Ids"
        :props="defaultProps">
    </el-tree>
    <el-button @click="getCheckNode">查看信息</el-button>
  </div>
</template>

<script>
  export default {
   props:["RoleId"],
    data() {
      return {
        data: [],
        defaultProps: {
          children: 'children',
          label: 'label'
        },
        Ids:[]
      };
    },
    methods:{
        //获取节点信息
        getMenu(){
                debugger;
                this.$axios.get("/api/Menu/GetMenuTreeAdd").then(
                    res=>{
                         var reg = new RegExp('\\,"children":\\[]', 'g')
                    this.data = JSON.parse(JSON.stringify(res.data).replace(reg, ''))
              })
        },
        // getCheckNode(){
        //     var list=this.$refs["treedate"].getCheckedNodes(true,true).map(m=>m.value);
        // },
        getDfCheck(){
            this.$axios.get(`/api/Role/GetMenuRoleResult?RoleId=${this.RoleId}`).then(
                res=>{
                  debugger;
                  console.log(res.data);
                  this.Ids=res.data.map(m=>m.MenuId);
                  console.log(this.Ids);
                }
          )
        }
    },
    created(){
        this.getMenu();
    },
    mounted(){
      this.getDfCheck();
    }
  };
</script>