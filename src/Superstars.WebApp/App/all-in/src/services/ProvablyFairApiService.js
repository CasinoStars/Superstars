import { postAsync, getAsync, getAsyncNoJSON } from "../helpers/apiHelper";
const endpoint = "/api/ProvablyFair";

class ProvablyFairApiService {
    constructor() {
    }

    async GetSeeds(){
        return await  getAsync(`${endpoint}/GetSeeds`);
    }


    async UpdateSeeds(){
        return await getAsync(`${endpoint}/UpdateSeeds`);
    }

    async CreateSeeds(){
        await  postAsync(`${endpoint}/CreateSeeds`);
    }

}

export default new ProvablyFairApiService();