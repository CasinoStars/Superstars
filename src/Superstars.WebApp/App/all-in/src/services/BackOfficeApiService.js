import { postAsync, getAsync} from "../helpers/apiHelper";
const endpoint = "/api/backoffice";

class BackOfficeApiService {
    constructor() {
    }
    
    async IsAdmin() {
        return await getAsync(`${endpoint}/isAdmin`);
    }
}

export default new BackOfficeApiService();