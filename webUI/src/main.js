// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import element from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import * as signalr from '@aspnet/signalr'
import  axios from  'axios'
/*import  '../assets/js/jquery.signalR-2.3.0'*/

Vue.use(element)
Vue.config.productionTip = false
Vue.prototype.signalr = signalr;
Vue.prototype.$ajax=axios;
/* eslint-disable no-new */
new Vue({
  el: '#app',
  components: { App },
  template: '<App/>'
})


