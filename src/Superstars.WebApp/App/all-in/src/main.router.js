// Vue router setup
import Vue from 'vue';
import VueRouter from 'vue-router';
Vue.use(VueRouter);

// Components
import Home from './components/Home.vue'

const routes = [
    { path: '', component: Home },
];

export default new VueRouter({
    mode: 'history',
    base: '/Home',
    routes
});