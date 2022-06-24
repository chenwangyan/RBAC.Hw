<template>
  <div>
    <el-container style="height:800px ; border: 1px solid #eee">
  <el-aside width="200px" style="background-color: rgb(238, 241, 246)">
     <el-menu
          :collapse="isCollapse"
          :default-active="activeIndex"
          :unique-opened="true"
          router
          ref="elMenu"
          @select="menuSelect"
        >
          <!-- 递归动态菜单 -->
          <myitem :data="menuArr"></myitem>
        </el-menu>
      
  </el-aside>
  
  <el-container>
    <el-header style="text-align: right; font-size: 12px">
      <el-dropdown>
        <i class="el-icon-setting" style="margin-right: 15px"></i>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item>查看</el-dropdown-item>
          <el-dropdown-item>新增</el-dropdown-item>
          <el-dropdown-item>删除</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
      <span>王小虎</span>
    </el-header>
    
    <el-main>
      <router-view/>
    </el-main>
  </el-container>
</el-container>
  </div>
</template>

<script>
import myitem from "../views/ZPage.vue";
export default {
  name: "Home",
  components: {
    myitem,
  },
  data() {
    return {
      isCollapse: false,

      activeIndex: this.$route.path,

      tableData:[],
      // 然后再通过权限管理页面，去添加或修改这个树结构，这样的话，前端就会呈现出来了
      menuArr: []
    };
  },
  mounted() {
    console.log(this.$route)
    this.showInfor();
  },
  methods: {
    menuSelect(index) {
      this.activeIndex = index;
    },
     showInfor()
        {
            this.$axios.get("/api/Menu/GetMenuList").then
            (
                res=>{
                    this.menuArr=res.data;
                    console.log(this.menuArr)
                }
                
            )
        },
    loginOut() {
      this.$router.push({
        path: "/login",
      });
      sessionStorage.removeItem("token");
    },
  },
};
</script>
