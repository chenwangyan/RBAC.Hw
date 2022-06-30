import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import(/* webpackChunkName: "about" */ '../views/Home.vue'),
    children:[
      {
        path: "/Menu",
        name: "Menu",
        component: () => import(/* webpackChunkName: "about" */ '../views/Menu/Menu.vue')
     },
     {
      path: "/Admin",
      name: "Admin",
      component: () => import(/* webpackChunkName: "about" */ '../views/Admin/Admin.vue')
     },{
      path: "/Role",
      name: "Role",
      component: () => import(/* webpackChunkName: "about" */ '../views/Role/Role.vue')
     },
    ]
  },
  {
    path: '/Login',
    name: 'Login',
    component: () => import(/* webpackChunkName: "about" */ '../views/Login.vue')
  }
]

const router = new VueRouter({
  routes
})

// // 导航守卫
// router.beforeEach((to, from, next) => {
//   var loginname=window.sessionStorage.getItem("UserName")??"";
//   if (to.name !== 'Login'&& loginname.length==0)
//   {
//     next({path:'/Login'});
//   }
//    next()
// })

export default router


