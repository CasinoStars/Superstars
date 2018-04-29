import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper';

const endpoint = "/api/user";

class UserApiService {
    constructor() {
    }
    
    async  Login() {
        return await postAsync(`${endpoint}/login`);
    }

    async register(model) {
        return await postAsync(`${endpoint}/Register`, model);
    }

};

export default new UserApiService();