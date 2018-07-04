import { postAsync, getAsync, getAsyncNoJSON } from "../helpers/apiHelper";
const endpoint = "/api/ProvablyFair";

class ProvablyFairApiService {
    constructor() {
    }

    async GetSeeds(){
        return await  getAsync(`${endpoint}/GetSeeds`);
    }


    async UpdateSeeds(str){
        return await postAsync(`${endpoint}/UpdateSeeds`, str);
    }

    async CreateSeeds(){
        await  postAsync(`${endpoint}/CreateSeeds`);
    }
    async RetriveDicesFromSeeds(clientSeedTest, serverSeedTest, nbOfDices){
        console.log(clientSeedTest);
        console.log(serverSeedTest);
        console.log(nbOfDices);
         return await postAsync(`${endpoint}/${clientSeedTest}/${serverSeedTest}/${nbOfDices}/RetriveDicesFromSeeds`)
    }

}

export default new ProvablyFairApiService();