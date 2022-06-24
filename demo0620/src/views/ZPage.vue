<template>
  <div>
    <template v-for="(item, index) in data">
      <!-- 因为有子集和无子集渲染html标签不一样，所以要分为两种情况
           情况一：有子集的情况：                         -->
      <el-submenu :key="index" :index="item.LinkUrl+''" v-if="item.children.length > 0">
        <template slot="title">
          <i :class="item.icon"></i>
          <span>{{ item.MenuName }}</span>
        </template>
        <myitem :data="item.children"></myitem>
      </el-submenu>
      <!-- 情况二：没子集的情况 -->
      <el-menu-item :key="index" v-else :index="item.LinkUrl+''">
        <i :class="item.icon"></i>
        <span slot="title">{{ item.MenuName }}</span>
      </el-menu-item>
    </template>
  </div>
</template>

<script>
export default {
  name: "myitem",
  props: {
    data: {
      type: Array,
      default: [],
    },
  },
  mounted(){
    debugger;
    console.log(this.data);
  }
 
  // 注意： 在template标签上使用v-for，:key="index"不能写在template标签上，因为其标签不会被渲染，会引起循环错误
};
</script>