import { postAsync, getAsyncNoJSON } from "../helpers/apiHelper";
const endpoint = "/api/settings";

class SettingsApiService {
    constructor() {

    }

    async updatePassword(oldPassword, newPassword) {
        return await postAsync(`${endpoint}/${oldPassword}/${newPassword}`);
    }

    async updateEmail(newMail) {
        return await postAsync(`${endpoint}/${newMail}`);
    }

    async getEmail() {
        return await getAsyncNoJSON(`${endpoint}/getMail`);
    }
}
export default new SettingsApiService();