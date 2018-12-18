import { postAsync, getAsync} from "../helpers/apiHelper";
const endpoint = "/api/crash";

class CrashApiService {
    constructor() {
    }
    
    async SendMessage(message) {
        return await postAsync(`${endpoint}/${message}`);
    }

    async GetNextCrash() {
        return await getAsync(`${endpoint}/getNextCrash`);
    }

    async GetPlayersInGame() {
        return await getAsync(`${endpoint}/GetPlayersInGame`);
    } 
    async GetHashList() {
        return await getAsync(`${endpoint}/HashList`);
    } 
}


export default new CrashApiService();