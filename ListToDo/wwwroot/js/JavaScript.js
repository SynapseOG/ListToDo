
    var f_register = document.getElementById("registerForm");
    f_register.addEventListener("submit", "validate");

    function validate(e) {
            var password = document.getElementById("pass");
        var repeatPassword = document.getElementById("repeat-pass");
        if (password != repeatPassword || password == "" || repeatPassword=="") {
        alert("Podane hasła są błędne lub brakuje danych formularza!")
                return false;
            }
    }
