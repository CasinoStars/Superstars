import * as types from '../mutation-types';

const state = {
    isLoading: false,
    BTCMoneyChange: false,
    fakeMoneyChange: false,
    errors: []
};

const mutations = {
    [types.SET_IS_LOADING](state, isLoading) {
        state.isLoading = isLoading;
    },

    [types.ERROR_HAPPENED](state, error) {
        state.errors.push(error || "");
    },

    [types.BTC_MONEY_CHANGE](state, BTCMoneyChange) {
        state.BTCMoneyChange = BTCMoneyChange;
    },

    [types.FAKE_MONEY_CHANGE](state, fakeMoneyChange) {
        state.fakeMoneyChange = fakeMoneyChange;
    }
};

export default {
    state,
    mutations
};