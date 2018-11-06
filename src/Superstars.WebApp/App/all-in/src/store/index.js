import Vue from 'vue';
import Vuex from 'vuex';
import Chat from 'vue-beautiful-chat'

import * as actions from './actions';
import * as getters from './getters';

import app from './modules/app';

Vue.use(Vuex);
Vue.use(Chat);

const debug = process.env.NODE_ENV !== 'production';

export default new Vuex.Store({
  actions,
  getters,
  modules: {
      app
  },
  strict: debug
})