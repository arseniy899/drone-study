from django.db import models
from django.utils.translation import gettext_lazy as _
from django.contrib.auth.models import User
from questions.models import Question


class Test(models.Model):
    name = models.CharField(max_length=256, verbose_name=_("Название теста"))
    description = models.TextField(verbose_name=_("Описание теста"))
    questions = models.ManyToManyField(
        Question, related_name="tests", verbose_name=_('Вопросы теста'),
        blank=True, default=None, through='TestQuestion'
    )

    def __str__(self):
        return f"{self.name}"

    class Meta:
        verbose_name = _("Тест")
        verbose_name_plural = _("Тесты")


class TestQuestion(models.Model):
    test = models.ForeignKey(Test, on_delete=models.CASCADE, verbose_name=_("Тест"))
    question = models.ForeignKey(Question, on_delete=models.CASCADE, verbose_name=_("Вопрос теста"))

    def __str__(self):
        return f"{self.question.short_name}"

    class Meta:
        verbose_name = _("Вопросы теста")
        verbose_name_plural = _("Вопросы теста")


class LearningResults(models.Model):
    test = models.OneToOneField(Test, on_delete=models.CASCADE, verbose_name=_('Тест'))
    users = models.ForeignKey(
        User, related_name="learning_results", on_delete=models.CASCADE, verbose_name=_('Пользователь')
    )

    def __str__(self):
        return f""

    class Meta:
        verbose_name = _("Результаты тестирования")
        verbose_name_plural = _("Результаты тестов")


class UserTestAnswers(models.Model):
    test_question = models.ForeignKey(
        TestQuestion, related_name="users_test_answers", on_delete=models.CASCADE,  verbose_name=_('Вопрос теста')
    )
    test_result = models.ForeignKey(
        LearningResults, related_name="test_answers", on_delete=models.CASCADE, verbose_name=_('Пользователь')
    )
    answer_correctly = models.BooleanField(default=False, verbose_name=_("Корректность ответа"))

    class Meta:
        verbose_name = _("Ответы пользователя")
        verbose_name_plural = _("Ответы пользователя")
