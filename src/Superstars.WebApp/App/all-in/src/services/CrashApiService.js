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

    async GetPlayersInGame(gameId) {
        return await getAsync(`${endpoint}/${gameId}/GetPlayersInGame`);
    } 
    async GetHashList() {
        return await getAsync(`${endpoint}/HashList`);
    } 
    async GetCrashMeta(gameId){
        return await getAsync(`${endpoint}/${gameId}/CrashMeta`)
    }
}


export default new CrashApiService();