import UserApiService from '../services/UserApiService';

/**
 * Filter for routes requiring an authenticated user
 * @param {*} to 
 * @param {*} from 
 * @param {*} next 
 */
export default function requireAuth(to, from, next) {
    if (!UserApiService.isConnected) {
        next({
            path: '/',
            query: { redirect: to.fullPath }
        });
        return;
    }
    next();
}