import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = "/api/user";

class UserApiService {
    constructor() {

    }
    
    async  IdentityVerify() {
        return await postAsync(endpoint);
    }

}

export default new UserApiService();