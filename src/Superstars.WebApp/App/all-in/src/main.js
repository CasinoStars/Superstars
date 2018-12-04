import 'babel-polyfill';
import 'whatwg-fetch';

import Vue from 'vue';
import store from './store';
import router from './main.router';
import App from './components/App.vue';

// Library
import 'jquery';
import 'popper.js';
import 'bootstrap';
import Element from 'element-ui';
Vue.use(Element, { size: 'small', zIndex: 3000 });

// Creation of the root Vue of the application
new Vue({
    el: '#app',
    router,
    store,
    render: h => h(App)
});