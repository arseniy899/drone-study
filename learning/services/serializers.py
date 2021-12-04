from rest_framework import serializers
from learning.models import Test
from questions.services.serializers import QuestionListSerializer


class TestSerializer(serializers.ModelSerializer):
    questions = QuestionListSerializer(read_only=True, many=True)

    class Meta:
        model = Test
        fields = ('name', 'questions', )
