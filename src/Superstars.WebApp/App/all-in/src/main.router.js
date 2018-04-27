// Vue router setup
import Vue from 'vue';
import VueRouter from 'vue-router';
Vue.use(VueRouter);

// Components
import Home from './components/Home.vue'
import Login from './components/Login.vue'
import Register from './components/Register.vue'

const routes = [
    { path: '', component: Home },
    { path: '/login', component: Login },
    { path: '/register', component: Register }
];

export default new VueRouter({
    mode: 'history',
    base: '/Home',
    routes
});