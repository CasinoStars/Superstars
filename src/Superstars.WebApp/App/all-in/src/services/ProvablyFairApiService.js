import { postAsync, getAsync, getAsyncNoJSON } from "../helpers/apiHelper";
const endpoint = "/api/wallet";

class ProvablyFairApiService {
    constructor() {
    }

    async GetSeeds(){
        return await getAsync(`${endpoint}/GetSeeds`);
    }

    async UpdateSeeds(){
        return await getAsync(`${endpoint}/UpdateSeeds`);
    }

}

export default new ProvablyFairApiService();