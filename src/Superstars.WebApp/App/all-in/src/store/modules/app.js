import * as types from '../mutation-types'

const state = {
    isLoading: false,
    walletChange: false,
    errors: []
}

const mutations = {
    [types.SET_IS_LOADING](state, isLoading) {
        state.isLoading = isLoading
    },

    [types.ERROR_HAPPENED](state, error) {
        state.errors.push(error || "")
    },

    [types.WALLET_CHANGE](state, walletChange){
        state.walletChange = walletChange
    }
}

export default {
    state,
    mutations
}