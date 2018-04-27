import 'babel-polyfill'

import Vue from 'vue';
import router from './main.router';
import App from './components/App.vue';

// Library
import 'jquery'
import 'popper.js'
import 'bootstrap'

// Creation of the root Vue of the application
new Vue({
    el: '#app',
    router,
    render: h => h(App)
});