from aiogram import Dispatcher
from aiogram import types
import api


from .Emoji import emoji_data


def setup(dp: Dispatcher):
    dp.register_callback_query_handler(estimate_callback, emoji_data.filter())


async def estimate_callback(call: types.CallbackQuery, callback_data: dict):
    estimate = callback_data['estimate']
    imageId = callback_data['imageId']
    await api.estimate(imageId, str(call.from_user.id), estimate)
    await call.answer()
    await call.message.answer('Принял, нажми /next для следующей картинки')
