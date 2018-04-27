import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper.js';

class UserApiService {
    constructor() {
        const endpoint = "/api/user/";
    }
    
    async  IdentityVerify() {
        return await getAsync(this.endpoint);
    }

};

export default new UserApiService();