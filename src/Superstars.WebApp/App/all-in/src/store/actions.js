import * as types from './mutation-types';
import services from '../services/WalletApiService';

/**
 * Notify when an error happens
 * @param {*} param0 
 * @param {*} error 
 */
export function notifyError({ commit }, error) {
    commit(types.ERROR_HAPPENED, error.message);
}

/**
 * Notify when an action is loading
 * @param {*} param0 
 * @param {boolean} isLoading
 */
export function notifyLoading({ commit }, isLoading) {
    commit(types.SET_IS_LOADING, isLoading);
}

// In order to be DRY, we can combine all of the previous actions in one action...

/**
 * Wraps an async function call in order to handle loading, and errors.
 * In case of error, the error is thrown.
 * @param {*} param0 
 * @param {*} asyncCallback The callback to be executed
 */
export async function executeAsyncRequest({ commit }, asyncCallback) {
    commit(types.SET_IS_LOADING, true);
    try {
        return await asyncCallback();
    }
    catch (error) {
        commit(types.ERROR_HAPPENED, error.message);
        throw error;
    }
    finally {
        commit(types.SET_IS_LOADING, false);
    }
}

export async function RefreshFakeCoins({ commit }) {
    try {
        commit(types.FAKE_MONEY, (await services.GetFakeBalance()).balance);
    }
    catch (error) {
        commit(types.ERROR_HAPPENED, error.message);
        throw error;
    }
}

export async function RefreshBTC({ commit }) {
    try {
        commit(types.BTC_MONEY, await services.GetTrueBalance());
    }
    catch (error) {
        commit(types.ERROR_HAPPENED, error.message);
        throw error;
    }
}