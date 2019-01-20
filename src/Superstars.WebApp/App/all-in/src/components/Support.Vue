
<template>
<div class="support">
    <div class ="form">
        <center><h1 style="color:white; padding-bottom:4%">Besoin d'aide ? s'il vous plait contactez nous </h1></center>

        <div class="field-wrap">
            <label>
                Email<span class="req">*</span>
            </label><br>    
            <input type="text" />
                         <label>
                Message<span class="req">*</span>
            </label><br>
            <div class="supportMessage"><input type="text"/></div>
        </div>
        <button type="submit" class="btn btn-outline-success mx-auto active" style="margin-top:2%">Envoyer</button>

    </div>
</div>
</template>


<script>
  export default {

   methods: {
      handleOpen(key, keyPath) {
        console.log(key, keyPath);
      },
      handleClose(key, keyPath) {
        console.log(key, keyPath);      
      },
   }
  }

</script>
<style lang="scss">

 $body-bg: #c1bdba;
  $form-bg: #13232f;
  $white: #ffffff;

  $main: #777c7b;
  $main-light: lighten($main, 5%);
  $main-dark: darken($main, 5%);

  $gray-light: #a0b3b0;
  $gray: #ddd;

  $thin: 300;
  $normal: 400;
  $bold: 600;
  $br: 4px;


    .support .form {
        background: rgba($form-bg, 0.9);
        padding: 40px;
        height: 900px;
        width: 1000px;
        margin: 40px auto;
        border-radius: $br;
        box-shadow: 0 4px 10px 4px rgba($form-bg, 0.3);
    }

    .support .field-wrap {
    color: white;
}

.support .supportMessage {
    height: 450px;
}

.support input {
    border-radius: 12px;
}

.support input,
  textarea {
    margin-bottom: 2%;
    margin-top: 1%;
    font-size: 22px;
    display: block;
    width: 100%;
    height: 100%;
    padding: 5px 10px;
    background: white;
    background-image: none;
    border: 1px solid $gray-light;
    transition: border-color 0.25s ease, box-shadow 0.25s ease;

    &:focus {
      outline: 0;
      border-color: $main;
    }
  }
</style>