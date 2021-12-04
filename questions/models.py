from django.db import models
from django.utils.translation import gettext_lazy as _


class QuestionCategory(models.Model):
    name = models.CharField(max_length=256, verbose_name=_("Название категории вопроса"))

    def __str__(self):
        return f"{self.name}"

    class Meta:
        verbose_name = _("Категория")
        verbose_name_plural = _("Категории вопросов")


class Question(models.Model):
    short_name = models.CharField(max_length=256, verbose_name=_("Короткое название вопроса"))
    wording = models.TextField(verbose_name=_("Формулировка вопроса"))
    category = models.ForeignKey(
        QuestionCategory, related_name="category", on_delete=models.CASCADE, verbose_name=_('Категория вопроса'),
        blank=True, default=None, null=True
    )

    def __str__(self):
        return f"{self.short_name}"

    class Meta:
        verbose_name = _("Вопрос")
        verbose_name_plural = _("Вопросы")


class QuestionsAnswers(models.Model):
    name = models.CharField(max_length=256, verbose_name=_("Название ответа"))
    is_correct = models.BooleanField(default=True, verbose_name=_("Корректность ответа"))
    question = models.ForeignKey(
        Question, related_name="answers", on_delete=models.CASCADE, verbose_name=_('Вопрос')
    )

    def __str__(self):
        return "Ответ на вопрос"
