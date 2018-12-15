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

// Creation of the root Vue of the application
new Vue({
    el: '#app',
    router,
    store,
    render: h => h(App)
});