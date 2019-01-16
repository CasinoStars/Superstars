import { postAsync, getAsync, deleteAsync} from "../helpers/apiHelper";
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

    async GetUsers() {
        return await getAsync(`${endpoint}/getUsers`);
    }

    async DeleteUser(userPseudo) {
        return await deleteAsync(`${endpoint}/${userPseudo}/deleteUser`);
    }
}

export default new BackOfficeApiService();