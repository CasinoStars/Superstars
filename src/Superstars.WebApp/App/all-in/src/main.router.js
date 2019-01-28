// Vue router setup
import Vue from 'vue';
import VueRouter from 'vue-router';
Vue.use(VueRouter);

import requireAuth from './helpers/requireAuth';

// Components
import Home from './components/Home.vue';
import Logout from './components/Logout.vue';
import Play from './components/Play.vue';
import Yams from './components/Yams.vue';
import BlackJack from './components/BlackJack.vue';
import Wallet from './components/Wallet.vue';
import Rules from './components/Rules.vue';
import Account from './components/Account.vue';
import Stats from './components/MyStats.vue';
import Rank from './components/Rank.vue';
import ProvablyFair from './components/ProvablyFair.vue';
import CrashTest from './components/CrashTest.vue';
import Faq from './components/Faq.vue';
import BackOffice from './components/BackOffice.vue';
import Transfer from './components/Transfer.vue';
import Settings from './components/Settings.vue';
import Support from './components/Support.vue';



const routes = [
    { path: '', component: Home },
    { path: '/logout', component: Logout, beforeEnter: requireAuth },
    { path: '/play', component: Play, beforeEnter: requireAuth },
    { path: '/yams', component: Yams, beforeEnter: requireAuth },
    { path: '/blackJack', component: BlackJack, beforeEnter: requireAuth },
    { path: '/wallet', component: Wallet, beforeEnter: requireAuth},
    { path: '/rule', component: Rules},
    { path: '/account', component: Account, beforeEnter: requireAuth},
    { path: '/statistics', component: Stats, beforeEnter: requireAuth},
    { path: '/playersStats', component: Rank},
    { path: '/Transfer', component: Transfer, beforeEnter: requireAuth},
    { path: '/provablyfair', component: ProvablyFair, beforeEnter: requireAuth},
    { path: '/crash', component: CrashTest},
    { path: '/faq', component: Faq},
    { path: '/BackOffice',component: BackOffice, beforeEnter: requireAuth}
];

export default new VueRouter({
    mode: 'history',
    base: '/Home',
    routes
});