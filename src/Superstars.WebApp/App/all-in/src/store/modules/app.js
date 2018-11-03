import * as types from '../mutation-types';

const state = {
    isLoading: false,
    BTCMoneyChange: false,
    fakeMoneyChange: false,
    fakeMoney: 0,
    BTCMoney: 0,
    errors: []
};

const mutations = {
    [types.SET_IS_LOADING](state, isLoading) {
        state.isLoading = isLoading;
    },

    [types.ERROR_HAPPENED](state, error) {
        state.errors.push(error || "");
    },

    [types.FAKE_MONEY](state, fakeMoney) {
        state.fakeMoney = fakeMoney;
    },

    [types.BTC_MONEY](state, BTCMoney) {
        state.BTCMoney = BTCMoney;
    }
};

export default {
    state,
    mutations
};