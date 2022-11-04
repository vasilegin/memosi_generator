from .consts import InlineConstructor
from aiogram.utils.callback_data import CallbackData
from data import config

emoji_data = CallbackData('estimate_image', 'imageId', 'estimate')


class Emoji(InlineConstructor):
    @staticmethod
    def emojiRating(imageId):
        schema = [len(config.emoji)]
        actions = [
            {'text': emoji, 'callback_data': emoji_data.new(imageId=imageId, estimate=estimate)} for emoji, estimate in sorted(config.emoji.items(), key=lambda x: x[1])
        ]
        return Emoji._create_kb(actions, schema)
