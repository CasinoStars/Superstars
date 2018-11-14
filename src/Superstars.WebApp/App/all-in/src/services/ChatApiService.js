import { postAsync, getAsync} from "../helpers/apiHelper";
const endpoint = "/api/chat";

class ChatApiService {
    constructor() {
    }


    async SendMessage(message) {
        return await postAsync(`${endpoint}/${message}`);
    }

    async GetMessageList() {
        return await getAsync(`${endpoint}/getMessages`);
    }

    
}

export default new ChatApiService();