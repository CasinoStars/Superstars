<template>
    <div class="settings">
        <!-- Modal Settings -->
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-hidden="false">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" style="margin-left:32%;">Gestion de compte</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div v-if="!editMail && !editPassword">
                        <div class="modal-body">
                            <ul class="tab-group">
                                <li class="tab"><a v-on:click="editPassword=true">Mot De Passe</a></li>
                                <li class="tab"><a v-on:click="getEmail()">Adresse Email</a></li>
                            </ul>
                        </div>
                        <div class="modal-footer"></div>
                    </div>
                    <form @submit="changePassword($event)" v-else-if="editPassword">
                        <div class="modal-body">
                            <div class="alert alert-danger" style="text-align: center; opacity: 0.7;" v-for="e of errors" :key="e">{{e}}</div>
                            <div class="field-wrap">
                                <label>
                                    Mot de Passe Actuel<span class="req">*</span>
                                </label><br>
                                <input type="password" v-model="item.oldPass" required />
                            </div>
                            <div class="field-wrap">
                                <label>
                                    Nouveau Mot de Passe<span class="req">*</span>
                                </label><br>
                                <input type="password" v-model="item.newPass" required />
                            </div>
                            <div class="field-wrap">
                                <label>
                                    Confirmer le Mot de Passe<span class="req">*</span>
                                </label><br>
                                <input type="password" v-model="item.confirmPass" required />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" v-if="editPassword" @click="cancel()" class="btn btn-outline-light mx-auto">Annuler</button>
                            <button type="submit" v-if="editPassword" class="btn btn-outline-success mx-auto">Sauvegarder</button>
                        </div>
                    </form>
                    <form @submit="changeEmail($event)" v-else>
                        <div class="modal-body">
                            <div class="alert alert-danger" style="text-align: center; opacity: 0.7;" v-for="e of errors" :key="e">{{e}}</div>
                            <div class="field-wrap">
                                <label>
                                    Adresse Mail<span class="req">*</span>
                                </label><br>
                                <input :placeholder="item.oldMail" v-model="item.newMail" required />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button v-if="editMail" type="button" @click="cancel()" class="btn btn-outline-light mx-auto">Annuler</button>
                            <button type="submit" v-if="editMail" class="btn btn-outline-success mx-auto">Sauvegarder</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    </div>
</template>

<script>
    import {
        mapActions
    } from 'vuex';
    import SettingsApiService from '../services/SettingsApiService';

    export default {
        data() {
            return {
                editPassword: false,
                editMail: false,
                item: {},
                errors: []
            };
        },

        methods: {
            ...mapActions(['executeAsyncRequest']),

            cancel() {
                this.errors = [];
                this.item = {};
                this.editPassword = false;
                this.editMail = false;
            },

            async changePassword(e) {
                e.preventDefault();
                this.errors = [];
                if (this.item.newPass.length < 6)
                    this.errors.push("Le mot de passe doit contenir au minimum 6 caracteres");
                else if (this.item.newPass.length > 100)
                    this.errors.push("Le mot de passe doit contenir au maximum 100 caracteres");
                else if (this.item.newPass != this.item.confirmPass)
                    this.errors.push("Le mot de passe de confirmation n'est pas bon");
                else {
                    var response = await this.executeAsyncRequest(() => SettingsApiService.updatePassword(this.item.oldPass, this.item.newPass).then(r => r.json()));
                    if (response == false)
                        this.errors.push("Le mot de passe actuelle est incorrecte");
                }
            },

            async getEmail() { 
                this.item.oldMail =  await this.executeAsyncRequest(() => SettingsApiService.getEmail());
                this.editMail = true;
            },

            async changeEmail(e) {
                e.preventDefault();
                this.errors = [];
                if(this.item.oldMail == this.item.newMail)
                    this.errors.push("Cette adresse mail est déja la votre");
                else {
                    var response = await this.executeAsyncRequest(() => SettingsApiService.updateEmail(this.item.newMail).then(r => r.json()));
                    if(!response)
                        this.errors.push("L'adresse mail n'est pas valide");
                }
            }
        }
    }
</script>

<style lang="scss">
$body-bg: #c1bdba;
$form-bg: #13232f;
$gray-light: #a0b3b0;
$white: #ffffff;
$main: #777c7b;
$main-dark: darken($main,5%);

.settings .modal-header {
    padding: 10px 16px;
    text-align: center;
    background: #222222a8;
    color: white;
}

.settings .modal-content {
    position: relative;
    background: rgba($form-bg,.9);
    margin: auto;
    padding: 0;
}

.settings .modal-body {
    text-align: center;
    input {
        width: 60%;
    }
}

.settings .modal-footer {
    min-height: 40px;
    padding: 10px 16px;
    background-color:  #222222a8;
    color: white;
}

.settings .field-wrap {
    color: white;
}

.settings .tab-group {
  padding:0;
  margin:0 0 5px 0;
  &:after {
    content: "";
    display: table;
    clear: both;
  }
  li a {
    text-decoration:none;
    background:rgba($gray-light,.25);
    color:$gray-light;
    font-size:20px;
    float:left;
    width:100%;
    text-align:center;
    cursor:pointer;
    transition:.5s ease;
    &:hover {
      background:$main-dark;
      color:$white;
    }
  }
}

.settings .close {
    color: white;
}
.settings {
    button{
        width: 30%;
    }
}
</style>