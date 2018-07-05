import { postAsync, getAsync, getAsyncNoJSON, postAxios } from "../helpers/apiHelper";
import AuthService from '../services/UserApiService';

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
        let pouilla;
        let zz = await postAxios(`${endpoint}/${clientSeedTest}/${serverSeedTest}/${nbOfDices}/RetriveDicesFromSeeds`).then(function(zz){
            console.log("show me that");
            pouilla = zz.data;
            console.log(zz);
            
        })
        
        return  pouilla;
        
        //   let a =  await postAsync(`${endpoint}/${clientSeedTest}/${serverSeedTest}/${nbOfDices}/RetriveDicesFromSeeds`).then(function(a){
        //     console.log(a );

        //     return a;

        //   });

         
    }

}

export default new ProvablyFairApiService();