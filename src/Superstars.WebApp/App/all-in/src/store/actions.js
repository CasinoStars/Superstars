import * as types from './mutation-types';

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
 * Notify when BTC wallet change
 * @param {*} param0 
 * @param {boolean} BTCMoneyChange
 */
export function notifyBTCMoneyChange({ commit }, BTCMoneyChange) {
    commit(types.BTC_MONEY_CHANGE, BTCMoneyChange);
}

/**
 * Notify when Fake wallet change
 * @param {*} param0 
 * @param {boolean} fakeMoneyChange
 */
export function notifyFakeMoneyChange({ commit }, fakeMoneyChange) {
    commit(types.FAKE_MONEY_CHANGE, fakeMoneyChange);
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

export async function executeAsyncRequestWithBTCMoney({ commit }, asyncCallback) {
    commit(types.BTC_MONEY_CHANGE, true);
    try {
        return await asyncCallback();
    }
    catch (error) {
        commit(types.ERROR_HAPPENED, error.message);
        throw error;
    }
    finally {
        commit(types.BTC_MONEY_CHANGE, false);
    }
}

export async function executeAsyncRequestWithFakeMoney({ commit }, asyncCallback) {
    commit(types.FAKE_MONEY_CHANGE, true);
    try {
        return await asyncCallback();
    }
    catch (error) {
        commit(type.ERROR_HAPPENED, error.message);
        throw error;
    }
    finally {
        commit(types.FAKE_MONEY_CHANGE, false);
    }
}