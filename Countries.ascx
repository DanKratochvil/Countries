<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Countries.ascx.cs" Inherits="Countries.Countries" %>
<script src="Scripts/jquery-3.4.1.min.js"></script>
<script type="text/javascript">
    $(document).ready(init);

    function init() {
        getCountries();
        $("#lbCountry").change(changeCountry);
        $("#lbLanguage").change(changeLanguage);
    }

    function getCountries() {
        $.getJSON("api/country",
            function (data) {
                $.each(data, function (index) {
                    $('#lbCountry').append('<option>' + data[index].name.common + '</option>');
                });
            });
    }

    function changeCountry() {
        var country = $("#lbCountry").val();
        $.getJSON("api/country/" + country,
            function (data) {
                $("#txtCapital").val(data.capital);
                var language = $("#lbLanguage").val();
                $("#txtTranslation").val(eval("data.translations." + language + ".common"));
                $("#lbBorder").empty();
                $.each(data.borders, function (index) {
                    $.getJSON("api/country/code/" + data.borders[index],
                        function (dt) {
                            $("#lbBorder").append('<option>' + dt.name.common + '</option>');
                        });                    
                });
            });
    }

    function changeLanguage() {
        var country = $("#lbCountry").val();
        var language = $("#lbLanguage").val();
        $.getJSON("api/country/" + country,
            function (data) {
                $("#txtTranslation").val(eval("data.translations." + language + ".common"));
            });
    }

</script>
<div class="row">
    <br />
    <div class="col-md-4">
        <h4>Country</h4>
        <select name="Country" id="lbCountry">
        </select>
    </div>
    <div class="col-md-4">
        <h4>Country translation</h4>
        <input type="text" id="txtTranslation" />
    </div>
    <div class="col-md-4">
        <h4>Country language</h4>
        <select name="Country" id="lbLanguage">
            <option value="deu">german</option>
            <option value="spa">spanish</option>
            <option value="fra">french</option>
            <option value="jpn">japanese</option>
            <option value="ita">italian</option>
        </select>
    </div>
</div>
<div>
    <h5>Capital</h5>
    <input type="text" id="txtCapital" />
    <h5>Border</h5>
    <select type="text" id="lbBorder" size="8" />
</div>

