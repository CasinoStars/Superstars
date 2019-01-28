<template>
    <div class="settings">
        <!-- Modal Settings -->
        <div class="tableau">
                <div class="form">
                    <div>
                                <h1>Gestion de compte</h1>
                                <div class="line" style="height:1px;"></div>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        </button>
                    </div>
                    <div>
                            <ul class="tab-group">
                                <div class="field-wrap" style="margin-top:3%; margin-bottom:3%;"><h3>Changer de mot de passe</h3></div>
                            </ul>
                    </div>
                    <form @submit="changePassword($event)">
                        <div>
                            <div class="alert alert-danger" style="text-align: center; opacity: 0.7;" v-for="e of errors" :key="e">{{e}}</div>
                            <div class="field-wrap">
                                <label>
                                    Ancien mot de passe<span class="req">*</span>
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
                            <button type="submit"  class="btn btn-outline-success mx-auto active">Sauvegarder</button>
                    </form>
                    <form @submit="changeEmail($event)">
                        <div>
                            <div class="alert alert-danger" style="text-align: center; opacity: 0.7;" v-for="e of errors" :key="e">{{e}}</div>
                            <div class="field-wrap">
                                <div class="field-wrap"><h3 style="margin-top:3%;margin-bottom:3%;">Changer d'adresse mail</h3></div>
                                <label>
                                    Adresse Mail<span class="req">*</span>
                                </label><br>
                                <input :placeholder="item.oldMail" v-model="item.newMail" required />
                            </div>
                        </div>
                        <div>
                            <button type="submit" class="btn btn-outline-success mx-auto active">Sauvegarder</button>
                        </div>
                    </form>
                </div>
            </div>    
        <div id="snackbar">{{success}} <i style="color:green" class="fa fa-check"></i></div>
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
                success: '',
                item: {},
                errors: []
            };
        },

        methods: {
            ...mapActions(['executeAsyncRequest']),


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
                    else {
                        // Put Message Succes
                        this.success = "Votre mot de passe a bien été changé"
                        // Get the snackbar DIV
                        var x = document.getElementById("snackbar");
                        x.className = "show";
                        // After 3 seconds, remove the show class from DIV
                        setTimeout(function(){ x.className = x.className.replace("show", ""); }, 3000);
                        this.cancel();
                    }

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
                    else {
                        // Put Message Succes
                        this.success = "Votre adresse mail a bien été changée"
                        // Get the snackbar DIV
                        var x = document.getElementById("snackbar");
                        x.className = "show";
                        // After 3 seconds, remove the show class from DIV
                        setTimeout(function(){ x.className = x.className.replace("show", ""); }, 3000);
                        this.cancel();
                    }
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
$br: 4px;


  .settings input,
  textarea {
    margin-bottom: 2%;
    margin-top: 1%;
    font-size: 22px;
    display: block;
    width: 100%;
    height: 100%;
    padding: 5px 10px;
    background: none;
    background-image: none;
    border: 1px solid $gray-light;
    color: $white;
    border-radius: 0;
    transition: border-color 0.25s ease, box-shadow 0.25s ease;

    &:focus {
      outline: 0;
      border-color: $main;
    }
  }

  .settings .tableau {
    background: rgba($form-bg, 0.9);
    padding: 40px;
    height: 900px;
    width: 1000px;
    border-radius: $br;
    box-shadow: 0 4px 10px 4px rgba($form-bg, 0.3);
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
  .myTitle {
    text-decoration:none;
    background:rgba($gray-light,.25);
    color:$gray-light;
    font-size:20px;
    float:left;
    width:100%;
    text-align:center;
    cursor:pointer;
    transition:.5s ease;
    color:white;

  }
}


.settings .close {
    color: white;
}

/* The snackbar - position it at the bottom and in the middle of the screen */
#snackbar {
    visibility: hidden; /* Hidden by default. Visible on click */
    min-width: 400px; /* Set a default minimum width */
    margin-left: -200px; /* Divide value of min-width by 2 */
    background-color: #333; /* Black background color */
    color: #fff; /* White text color */
    text-align: center; /* Centered text */
    border-radius: 10px; /* Rounded borders */
    padding: 16px; /* Padding */
    position: fixed; /* Sit on top of the screen */
    z-index: 1; /* Add a z-index if needed */
    left: 50%; /* Center the snackbar */
    bottom: 30px; /* 30px from the bottom */
}

/* Show the snackbar when clicking on a button (class added with JavaScript) */
#snackbar.show {
    visibility: visible; /* Show the snackbar */
    /* Add animation: Take 0.5 seconds to fade in and out the snackbar. 
   However, delay the fade out process for 2.5 seconds */
   -webkit-animation: fadein 0.5s, fadeout 0.5s 2.5s;
   animation: fadein 0.5s, fadeout 0.5s 2.5s;
}

/* Animations to fade the snackbar in and out */
@-webkit-keyframes fadein {
    from {bottom: 0; opacity: 0;} 
    to {bottom: 30px; opacity: 1;}
}

@keyframes fadein {
    from {bottom: 0; opacity: 0;}
    to {bottom: 30px; opacity: 1;}
}

@-webkit-keyframes fadeout {
    from {bottom: 30px; opacity: 1;} 
    to {bottom: 0; opacity: 0;}
}

@keyframes fadeout {
    from {bottom: 30px; opacity: 1;}
    to {bottom: 0; opacity: 0;}
}
</style>