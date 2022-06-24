import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import axios from 'axios';

const service = axios.create({
  // timeout:10000
  baseURL:"https://localhost:44343" //这里写自己的api地址
})
Vue.config.productionTip = false
Vue.use(ElementUI);
 Vue.prototype.$axios=service;
// 拦截器





new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
