import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'

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
    ]
  },
  {
    path: '/about',
    name: 'about',
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  },,
  {
    path: '/Menu',
    name: 'Menu',
    component: () => import(/* webpackChunkName: "about" */ '../views/Menu/Menu.vue')
  }
]

const router = new VueRouter({
  routes
})

//导航守卫
// router.beforeEach((to, from, next) => {
//   var loginname=window.sessionStorage.getItem("")??"";
//   if (to.name !== 'Login'&& loginname.length==0)
//   {
//     next({name:'Login'});
//   }
//    next()
// })

export default router


