$(function () {
    $('#profileContent .messagesPanel textarea').keypress(function (e) {
        if (e.which === 10 || e.which == 13 && e.ctrlKey) {
            var $textarea = $(this);
            var text = $.trim($textarea.val());

            if (text !== '') {
                var userId = $('#profileContent input[name=userId]').val();

                $textarea.attr('disabled', 'disabled');
                $textarea.val('Enviando...');

                $.post('/Message/Create',
                    {
                        userId: userId,
                        text: text
                    },
                    function (response) {
                        var $list = $('#profileContent .messagesPanel .list');

                        var date = response.CreatedOn.jsonToDate();
                        var listItem = '<div class="item"><input type="hidden" name="messageId" value="{5}" /><a href="/Perfil/{0}"><img src="data:image/jpg;base64,{1}" alt="Imagem do usuário" title="{2}" /></a><p>{3}</p><p class="date">{4} <input type="button" name="remove" class="remove" value="Excluir" /></p></div>'.format(response.From.Id,
                                                      response.From.Picture,
                                                      response.From.Name,
                                                      response.Text,
                                                      date.format("dd/mm/yyyy HH:MM"),
                                                      response.Id);

                        $list.prepend(listItem);

                        $textarea.attr('disabled', '');
                        $textarea.val('');
                    }
                );
            }
        }
    });

    $('#profileContent .messagesPanel .remove').live('click', function () {
        var messageId = $(this).parent().parent().find('[name=messageId]').val();

        $.post('/Message/Remove/' + messageId,
            function (response) {
                var $list = $('#profileContent .messagesPanel .list');

                $list.find('[name=messageId][value={0}]'.format(messageId)).parent().remove();
            }
        );
    });
});