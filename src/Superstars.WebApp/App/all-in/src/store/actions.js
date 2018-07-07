import * as types from './mutation-types'

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

/**
 * Notify when wallet change
 * @param {*} param0 
 * @param {boolean} walletChange
 */
export function notifyWalletChange({ commit }, walletChange) {
    commit(types.WALLET_CHANGE, walletChange);
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

export async function executeAsyncRequestWithMoney({ commit }, asyncCallback) {
    commit(types.WALLET_CHANGE, true);
    try {
        return await asyncCallback();
    }
    catch (error) {
        commit(types.ERROR_HAPPENED, error.message);
        throw error;
    }
    finally {
        commit(types.WALLET_CHANGE, false);
    }
}