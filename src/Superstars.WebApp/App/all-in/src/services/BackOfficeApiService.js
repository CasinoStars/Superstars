import { postAsync, getAsync} from "../helpers/apiHelper";
const endpoint = "/api/backoffice";

class BackOfficeApiService {
    constructor() {
    }
    
    async IsAdmin() {
        return await getAsync(`${endpoint}/isAdmin`);
    }

    async GetLogs() {
        return await getAsync(`${endpoint}/getLogs`);
    }
}

export default new BackOfficeApiService();