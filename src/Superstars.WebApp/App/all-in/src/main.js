import Vue from 'vue';
import router from './main.router';
import App from './components/App.vue';


// Creation of the root Vue of the application
new Vue({
    el: '#app',
    router,
    render: h => h(App)
});