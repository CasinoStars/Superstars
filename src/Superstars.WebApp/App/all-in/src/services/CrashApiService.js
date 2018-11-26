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
}

export default new CrashApiService();