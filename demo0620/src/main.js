import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import axios from 'axios';
import VueCookies from "vue-cookies";

const service = axios.create({
  // timeout:10000
  baseURL:"http://localhost:29610", //这里写自己的api地址
})
//认证
service.defaults.withCredentials = true

service.defaults.headers.common['Authorization'] = `bearer ${localStorage.getItem("LoginToken")}`

Vue.config.productionTip = false
Vue.use(ElementUI);
Vue.use(VueCookies);
 Vue.prototype.$axios=service;
// 拦截器


new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
