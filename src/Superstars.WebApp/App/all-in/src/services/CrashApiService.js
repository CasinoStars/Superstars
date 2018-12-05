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
}

export default new CrashApiService();