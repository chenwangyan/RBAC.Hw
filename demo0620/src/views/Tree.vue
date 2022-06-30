<template>
  <div>
    <el-tree
    ref="treedate"
        :data="data"
        show-checkbox
        node-key="id"
        :default-expand-all="true"
        :default-expanded-keys="[2, 3]"
        :default-checked-keys="[5]"
        :props="defaultProps">
    </el-tree>
    <el-button @click="getCheckNode">查看信息</el-button>
  </div>
</template>

<script>
  export default {
   
    data() {
      return {
        data: [],
        defaultProps: {
          children: 'children',
          label: 'label'
        }
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
        getCheckNode(){
            var list=this.$refs["treedate"].getCheckedNodes(true,true).map(m=>m.value);
        }
    },
    created(){
        this.getMenu();
    }
  };
</script>