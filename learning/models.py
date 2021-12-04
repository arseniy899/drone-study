from django.db import models
from django.utils.translation import gettext_lazy as _
from django.contrib.auth.models import User
from questions.models import Question


class Test(models.Model):
    name = models.CharField(max_length=256, verbose_name=_("Название теста"))
    description = models.TextField(verbose_name=_("Описание теста"))
    questions = models.ManyToManyField(
        Question, related_name="questions", verbose_name=_('Вопросы теста'),
        blank=True, default=None
    )

    def __str__(self):
        return f"{self.name}"

    class Meta:
        verbose_name = _("Тест")
        verbose_name_plural = _("Тесты")


class LearningResults(models.Model):
    test = models.OneToOneField(Test, on_delete=models.CASCADE, verbose_name=_('Тест'))
    users = models.ManyToManyField(
        User, related_name="learning_results", verbose_name=_('Пользователь')
    )

    def __str__(self):
        return f""

    class Meta:
        verbose_name = _("Результаты тестирования")
        verbose_name_plural = _("Результаты тестов")
